# ChessMeters
Analizes your chess games

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
