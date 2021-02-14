# ChessMeters
Analizes your chess games

## Stockfish
position startpos moves e2e4 e7e5
go depth 15

## Start the webserver:

cd ChessMeters.Web

dotnet run

// dotnet run --urls "http://chessmeters.gomiliare.com/"

## Entity Framework:
/root/.dotnet/tools/dotnet-ef migrations add AddedGameProperties
/root/.dotnet/tools/dotnet-ef migrations remove

## Database update:

cd ChessMeters.Core

export PATH="$PATH:/root/.dotnet/tools"

dotnet ef database update

## Tests:

cd ChessMeters.Core.Tests

dotnet test

## Linux Setup:

npm install -g @angular/cli

## Linux / DB:

sudo ~/.dotnet/tools/dotnet-ef database update

## Google and Facebook authentication:

dotnet user-secrets set "Authentication:Google:ClientId" "&lt;client-id&gt;"
  
dotnet user-secrets set "Authentication:Google:ClientSecret" "&lt;client-secret&gt;"

dotnet user-secrets set "Authentication:Facebook:AppId" "&lt;app-id&gt;"

dotnet user-secrets set "Authentication:Facebook:AppSecret" "&lt;app-secret&gt;"

## TODO
- Fix user colors when editing PGN in report generator
- Configure email for contact, error notifications and user registrations
- Evaluation centipawns for mate, maybe add notes to EngineEvaluations
- Configure lichess api with more parameters
- PROD environments for swap
- Notation for games on UI
- Chessboard game to PGN and analyze
- CI/CD fail to commit if unit tests are not passing
- Admin UI to change flags
- Light/dark themes (maybe from Bootstrap)
- Docker images for Linux and Windows
- Add sorting to whatever columns we need to sort by in grids

