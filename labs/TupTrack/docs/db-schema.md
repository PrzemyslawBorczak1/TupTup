# TupTrack database schema 


```mermaid
erDiagram
    RECORDING ||--o{ TIMESTAMP_LABEL : has
    RECORDING ||--o| NOTE : has
    RECORDING }o--|| RECORDING_GROUP : is
    LABEL_TYPE ||--o{ TIMESTAMP_LABEL : classifies
    RECORDING ||--o{ SENSOR : has
    SENSOR_TYPE ||--o{ SENSOR : typed_as
    SENSOR ||--o{ SENSOR_READING : produces

    RECORDING {
        int id PK
        int group_type FK
        datetime started_at
        datetime ended_at
        int duration_ms
        boolean is_local
    }

    SENSOR {
        bigint id PK
        int recording_id FK
        int sensor_type_id FK
        float sample_rate_hz
    }

    SENSOR_TYPE {
        int id PK
        int attributes FK
        string code
        string name
        string unit
    }

    SENSOR_READING {
        bigint id PK
        bigint sensor_id FK
        datetime timestamp
        int offset_ms
        float val
    }

    LABEL_TYPE {
        int id PK
        string name
        string description
    }

    TIMESTAMP_LABEL {
        bigint id PK
        int recording_id FK
        int label_type_id FK
        int at_offset_ms
        string description "nullable"
    }

    NOTE {
        bigint id PK
        int recording_id FK
        string content
    }

    RECORDING_GROUP{
        bigint id PK
        string name
        string description
    }
```

## Notes
- In this schema, only one group can be assigned to a recording.
- Only one note can be assigned to a recording.
- It will be possible to add a description to an event.
- Wi-Fi, Bluetooth, and other named sensors will be stored as attributes in `SensorType`.
  - TODO: add these attributes.
- The sampling rate may change while the application is running, for example when it is moved to the background, so the rate is stored in the `Sensor` table rather than in `SensorType`.
- There are two possible ways to handle sensors that output data in more than one dimension:
  1. represent each dimension as a separate sensor type,
  2. store multiple values in `SensorReading`, for example using nullable fields such as `y`, `z`, `v4`, `v5`, and `v6`.
- It is possible that some additional fields related to the upload state of a recording will be required.
- It is not yet clear what is better in `SensorReading`: storing an offset or a datetime value. It is also possible that this will not be enough to sort readings in the correct order, so there may need to be an additional field indicating the reading number.
