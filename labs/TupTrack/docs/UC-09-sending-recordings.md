# UC-09 User can send saved recordings

## Goal
To allow the user to send saved recordings to remote storage.

## Preconditions
- The application is running.
- There are recordings marked as local. Local recordings are recordings stored only on the device.

## Main Scenario
1. The user starts the upload process.
2. The system sends all recordings marked as local to remote storage.
3. The system waits for a response.
4. If the response confirms success, the system marks the uploaded recordings as no longer local.

## Postconditions
- The uploaded recordings are marked as no longer local.

## Notes
- It may be useful to allow the user to delete local recordings after a successful upload.