# TupTrack database schema 


```mermaid
erDiagram
    RECORDING ||--o{ TUP_STATE : has
    RECORDING ||--o{ ROOM_TIMESTAMP : has
    ROOMS ||--o{ ROOM_TIMESTAMP : has
    RECORDING }o--|| RECORDING_GROUP : is
    RECORDING ||--|{ SENSOR_READING : has
    SENSOR_READING }o--|| SENSOR_TYPE : is

    RECORDING {
        int id PK
        int group_type FK "nullable"
        datetime started_at
        datetime ended_at "nullable"
        string note "nullable"
    }

    SENSOR_READING {
        int id PK
        int recording_id FK
        int sensor_type FK
        datetime timestamp
        float val
    }

    SENSOR_TYPE {
        int id PK
        string name
        int dim
        string json_spec "nullable"
    }

    TUP_STATE  {
        int id PK
        int recording_id FK
        tup_state state
        datetime from_timestamp
        string description "nullable"
    }

    ROOMS{
        string name PK
        string description "nullable"
    }

    ROOM_TIMESTAMP  {
        int id PK
        string room_id FK
        int recording_id FK
        datetime from_timestamp
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
- If there will be decision to virtuali synchornise additional fields will be needed

