# Source Code Overview

| Layer | Contents |
| ----- | -------- |
| Presentation Layer | Contains API and WebApp (Blazor) |
| Business Layer | Contains all business decision logic, such as exception handling and orchestration classes | 
| Domain Layer | Contains all entity definitions as well as all contract definitions used within the domain of PuppyTracking | 
| Data Layer | Contains IO specific implentations of CRUD operations as well as anti-corruption code |
| Cross-Domain Layer | Contains code that is reused across all the other layers | 