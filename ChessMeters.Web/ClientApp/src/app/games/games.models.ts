export interface Game {
  id: number;
  moves: string;
  result: string;
}

export interface GameDetails {
  treeMoves: TreeMove[];
}

interface TreeMove {
  stockfishEvaluationCentipawns: number;
  move: string;
}
