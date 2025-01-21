
![simpel](https://github.com/user-attachments/assets/5a5c2808-f312-4bdd-827c-0a76afe215d3)

**ASP.NET Minimal API** + **VITE/REACT**

Fullstack app for retrieving, processing and presenting electricity daily price in format:
*- Daily Average Price*

*- Daily Highest Price*
*x Percentual Increase from Daily Avg*

*- Daily Lowest Price*
*- Percentual Decrease from Daily Avg*

## Setup
1. `cd simpel.api`
2. `dotnet run`
3. `cd frontend`
4. `npm install`
5. `npm run dev`

Debug:
- Backend Port is set to *5184* by default (set in Properties/**launchSettings.json**)
- Vite port is set to *5173* by default (set in **vite.config.js**)
- Make sure CORS policy in **Program.cs** tolerates REACT port on *line 12*:
`policy.WithOrigins("http://localhost:5173");`
