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
