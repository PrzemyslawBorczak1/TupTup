using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using Core.Core.Segments;
using Core.Core.Segments.Rules;
using IndoorLocalization.Trl3.Core.Motion;
using IndoorLocalization.Trl3.Core.Time;
using IndoorLocalization.Trl3.Core.Gyro;

namespace App;

public partial class MotionLabPage : ContentPage
{
	private long _tickIndex;
	public long TickIndex
	{
		get => _tickIndex;
		set { _tickIndex = value; OnPropertyChanged(); }
	}
	
	private string _eventsText = string.Empty;
	public string EventsText
	{
		get => _eventsText;
		set { _eventsText = value; OnPropertyChanged(); }
	}

	private string _segmentsText = string.Empty;
	public string SegmentsText
	{
		get => _segmentsText;
		set { _segmentsText = value; OnPropertyChanged(); }
	}

	private string _turnsText = string.Empty;
	public string TurnsText
	{
		get => _turnsText;
		set { _turnsText = value; OnPropertyChanged(); }
	}
	
	
	private CompositeDisposable _disposables = new();
	private ITickSource? _tickSource;
	private AndroidGyroSource? _gyroSource;
	private AndroidAccelerationSource? _accelSource;
	private AndroidPressureSource? _pressureSource;
	
	private readonly List<MotionEvent> _motionEvents = new();
	private readonly List<TurnEvent> _turns = new();
	private readonly List<Segment> _segments = new();
	private volatile bool _isDisappeared;
	
	
	public MotionLabPage()
	{
		InitializeComponent();
		
		BindingContext = this;
		
		// inicjalny tekst (czytelny TRL-3)
		EventsText = "  (no events)";
		TurnsText = "  (no turns)";

	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_isDisappeared = false;
		_disposables = new CompositeDisposable();

		// =========================
		// 1. GLOBAL TICK (1 Hz)
		// =========================

		_tickSource = new RxTickSource();

		var time =
			_tickSource.Ticks
				.Select(t => TimeSpan.FromSeconds(t.Index));

		_tickSource.Ticks
			.Subscribe(t =>
				MainThread.BeginInvokeOnMainThread(() => TickIndex = t.Index))
			.DisposeWith(_disposables);

		_tickSource.Start();

		// =========================
		// 2. ACCELERATION → MOVING / STOPPED
		// =========================

		_accelSource =
			new AndroidAccelerationSource(
				Android.App.Application.Context);

		_accelSource.Start();

		var motionFromAccel =
			new MotionFromAccelPipeline(
				_accelSource,
				window: TimeSpan.FromSeconds(1.5),
				varianceThreshold: 0.08);

		// =========================
		// 3. PRESSURE → VERTICAL
		// =========================

		_pressureSource =
			new AndroidPressureSource(
				Android.App.Application.Context);

		_pressureSource.Start();

		var verticalPipeline =
			new VerticalMotionPipeline(
				_pressureSource,
				window: TimeSpan.FromSeconds(3),
				deltaThreshold: 0.5f);

		// =========================
		// 4. MERGE → MOTION STATE
		// =========================

		var motionStates =
			Observable.Merge(
					motionFromAccel.States,
					verticalPipeline.States)
				.DistinctUntilChanged();

		// =========================
		// 5. MOTION EVENTS (E2)
		// =========================

		var motionEvents =
			motionStates.ToMotionEvents(time);

		motionEvents
			.Subscribe(OnMotionEvent)
			.DisposeWith(_disposables);
		
		
		// ==================================
		// 6. SEGMENTS (E3)
		// ==================================

		var rules = new ISegmentRule[]
		{
			new ElevatorRule(),
			new CorridorRule(),
			new StopRule()
		};
		
		var segmentBuilder =
			new SegmentBuilder(motionEvents, rules);

		segmentBuilder.Segments
			.Subscribe(OnSegment)
			.DisposeWith(_disposables);

		// =========================
		// 7. GYRO → TURN DETECTION
		// =========================

		_gyroSource =
			new AndroidGyroSource(
				Android.App.Application.Context);

		_gyroSource.Start();

		var turnPipeline =
			new TurnDetectionPipeline(
				_gyroSource,
				window: TimeSpan.FromMilliseconds(800),
				omegaThreshold: 0.4);

		turnPipeline.Turns
			.Subscribe(OnTurn)
			.DisposeWith(_disposables);
		
		
	}
	
	private void OnMotionEvent(MotionEvent e)
	{
		if (_isDisappeared) return;

		_motionEvents.Add(e);

		// ogranicz historię (TRL-3: ostatnie N)
		if (_motionEvents.Count > 20)
			_motionEvents.RemoveAt(0);

		MainThread.BeginInvokeOnMainThread(UpdateEventsText);
	}
	
	private void OnTurn(TurnDirection direction)
	{
		if (_isDisappeared) return;

		var turnEvent = new TurnEvent(
			Direction: direction,
			Time: TimeSpan.FromSeconds(TickIndex));

		_turns.Add(turnEvent);

		if (_turns.Count > 10)
			_turns.RemoveAt(0);

		MainThread.BeginInvokeOnMainThread(UpdateTurnsText);
	}
	
	private void OnSegment(Segment segment)
	{
		if (_isDisappeared) return;

		_segments.Add(segment);

		// TRL-3: ograniczamy historię do ostatnich N segmentów
		if (_segments.Count > 10)
			_segments.RemoveAt(0);

		MainThread.BeginInvokeOnMainThread(UpdateSegmentsText);
	}

	
	
	private void UpdateEventsText()
	{
		if (_isDisappeared) return;
		if (_motionEvents.Count == 0)
		{
			EventsText = "  (no events)";
			return;
		}

		EventsText =
			string.Join(
				"\n",
				_motionEvents.Select(e =>
					$"  [{e.Time:mm\\:ss}] {e.From} → {e.To}")
			);
	}
	
	private void UpdateTurnsText()
	{
		if (_isDisappeared) return;
		if (_turns.Count == 0)
		{
			TurnsText = "  (no turns)";
			return;
		}

		TurnsText =
			string.Join(
				"\n",
				_turns.Select(t => $"  [{t.Time:mm\\:ss}] {t.Direction}")
			);
	}
	
	private void UpdateSegmentsText()
	{
		if (_isDisappeared) return;
		if (_segments.Count == 0)
		{
			SegmentsText = "  (no segments)";
			return;
		}

		SegmentsText =
			string.Join(
				"\n",
				_segments.Select(s =>
					$"  {s.Type}: {s.Start:mm\\:ss} – {s.End:mm\\:ss} ({s.Duration.TotalSeconds:F0} s)")
			);
	}

	

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		_isDisappeared = true;

		_disposables.Dispose();

		_tickSource?.Dispose();
		_tickSource = null;

		_accelSource?.Dispose();
		_accelSource = null;

		_pressureSource?.Dispose();
		_pressureSource = null;

		_gyroSource?.Dispose();
		_gyroSource = null;
	}
}
