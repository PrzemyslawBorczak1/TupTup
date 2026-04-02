# UC-01 User can create descriptive labels during recording

## Goal
To label sensor readings for later analysis.

## Preconditions
- The system is in the recording state.
- The user is able to create or select labels such as:
  - Note
  - Group

## Main Scenario
1. The user creates or selects a label during recording.
2. The system assigns the label to the current recording or to the next recorded segment.
3. Labels are saved with accoring recording

## Postconditions
- A record of the captured sensor activity is stored in the database together with its labels.

## Notes
- Labels such as Group should be predefined.