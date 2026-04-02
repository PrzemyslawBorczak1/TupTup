# UC-01 User can record sensor data

## Description
This is the main purpose of the application.

## Goal
To store real-world sensor readings for later analysis.

## Preconditions
- The application is running.
- Device sensors are available.
- The application has permission to access sensor data.
- The system is able to collect sensor data.

## Main Scenario
1. The user starts recording.
2. The system saves metadata such as the recording start time and the types of available sensors.
3. The system starts recording data from the available sensors.
4. The system stores the sensor readings.
5. The user stops recording.
6. The system saves the recorded data from each sensor in the database together with metadata such as the recording duration and sensor types. The recording is also marked as local.

## Alternative Scenario: Application is moved to the background
After step 4 of the main scenario:
1. The application is moved to the background.
2. If background recording is supported, the system continues collecting sensor data for a limited period of time.
3. If background recording is not supported, the system stops recording.
4. When recording stops, the system saves the collected data according to step 6 of the main scenario.

## Postconditions
- A record of the captured sensor activity is stored in the database.

## Notes
- The system behavior after the application is moved to the background depends on the implementation.
- If running in the background causes a significant drop in data quality, recording should be stopped and the collected data should be saved.
- If no such issue is observed, the application may continue recording in the background.