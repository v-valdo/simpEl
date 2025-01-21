**ASP.NET Minimal API** + **VITE/REACT**

Fullstack app for retrieving, processing and presenting electricity daily price in format:
*x Daily Average Price*

*x Daily Highest Price*
*x Percentual Increase from Daily Avg*

*x Daily Lowest Price*
*x Percentual Decrease from Daily Avg*

## Setup
1. `cd simpel.api`
2. `dotnet run`
3. `cd frontend`
4. `npm install`
5. `npm run dev`

Debug:
- Backend Port is set to *5184* by default (set in Properties/**launchSettings.json**)
- Vite port is set to *5173* by default (set in **vite.config.js**)
- Make sure CORS policy in **Program.cs** tolerated REACT port on *line 12*:
`policy.WithOrigins("http://localhost:5173");`