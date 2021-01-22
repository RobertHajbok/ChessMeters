export interface Game {
  id: number;
  moves: string;
  result: string;
  event: string;
}

export interface GameDetails {
  treeMoves: TreeMove[];
  event: string;
}

interface TreeMove {
  stockfishEvaluationCentipawns: number;
  move: string;
}
