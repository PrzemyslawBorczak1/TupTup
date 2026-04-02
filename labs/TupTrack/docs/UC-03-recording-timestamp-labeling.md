# UC-03 During recoring user can creat timestamp labels


## Goal
To lable sensor readings for later analysis.

## Preconditions
- System is in recording state
- User is able to choose dofferent labels like: 
    - Stairs, Elevator, Flat
    - Room, Event



## Main Scenario
1. User starts recording
2. User chooses label
3. System saves point in time when label was chooosen
4. Steps 2 and 3 may be repeated multiple times during the recording.
5. The reocrding is stopped
6. The systems saves recording with labels

## Alternative scenario
1. Label was choosen before starting the recording. It shoul dbe treated as it was choosen at 0::00 timeline


## Postconditions
- A record of the captured sensor activity is stored in the database with labels

## Note
- Some of the labels (like room) should be choosen from predefined options or option for adding new room shoudl be present 
