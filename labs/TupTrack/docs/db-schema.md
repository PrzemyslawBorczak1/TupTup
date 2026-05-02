# TupTrack database schema 


```mermaid
erDiagram
    RECORDING ||--o{ TIMESTAMP_LABEL : has
    RECORDING }o--|| RECORDING_GROUP : is
    LABEL_TYPE ||--o{ TIMESTAMP_LABEL : classifies
    RECORDING ||--|{ SENSOR_READING : has
    SENSOR_READING }o--|| SENSOR_TYPE : is

    RECORDING {
        int id PK
        int group_type FK
        datetime started_at
        datetime ended_at
        int duration_ms
        boolean is_local
        string note "nullable"
    }

    SENSOR_READING {
        bigint id PK
        bigint recording_id FK
        bigint sensor_type FK
        datetime timestamp
        float val
    }

    SENSOR_TYPE {
        bigint id PK
        string name
        int dim
        string json_spec "nullable"
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
        datetime timestamp
        string description "nullable"
    }

    RECORDING_GROUP{
        string name PK
        string description
    }
```

## Notes
- Wi-Fi, Bluetooth, and other named sensors will be stored as attributes in `SensorType`.
  - TODO: add these attributes.
- It is possible that some additional fields related to the upload state of a recording will be required.

