# UC-01 User can view basic sensor readings

## Goal
To allow the user to check whether the application is correctly reading data from the available sensors.

## Preconditions
- The application is running.
- Device sensors are available.
- The application has permission to access sensor data.

## Main Scenario
1. The user opens the application.
2. The system starts collecting data from the available sensors.
3. The system displays the current sensor readings.
4. The system stores recent readings in memory.
5. The system presents recent readings.

## Alternative Scenario: User starts recording
At step 5 of the main scenario:
1. The user starts recording.
2. The system resets the previously stored preview readings.
3. The system starts storing recorded readings.
4. The system displays only the recorded readings.

## Postconditions
- The current sensor readings are visible to the user.
- A short history of recent readings is available for preview.
- If recording is started, only readings collected during the recording session are displayed.

## Notes
- Only a limited time window of recent readings is displayed.
- The exact caching duration depends on the implementation.
- A real-time updated chart may be the most effective way to present the readings.
- The preview window is reset when the user starts recording.