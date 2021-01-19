# ChessMeters
Analizes your chess games

## Stockfish
position startpos moves e2e4 e7e5
go depth 15

## Start the webserver:

cd ChessMeters.Web

dotnet run

// dotnet run --urls "http://chessmeters.gomiliare.com/"

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

dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"
  
dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"

dotnet user-secrets set "Authentication:Facebook:AppId" "<app-id>"

dotnet user-secrets set "Authentication:Facebook:AppSecret" "<app-secret>"

## TODO
- PGN convert to uci moves using pgn-extract
- After extracting with pgn-extract, insert to DB in Games table
- Analyze games from Games table
- Fix authentication
- PROD environments
- Notation for games on UI
- Upload PGN from file to analyze
- Chessboard game to PGN and analyze
- Chart for centipawns on UI
- Login with Facebook
- Grid to view uploaded games from PGN
- Light/dark themes (maybe from Bootstrap)
- CI/CD fail to commit if unit tests are not passing
- Report tables, grid on UI, possibility to create, report details, game details etc.
- Admin UI to change flags
- Traffic logging
- Docker images for Linux and Windows
