export interface Game {
  id: number;
  moves: string;
  result: string;
  event: string;
  site: string;
}

export interface GameDetails {
  treeMoves: TreeMove[];
  event: string;
  site: string;
}

interface TreeMove {
  stockfishEvaluationCentipawns: number;
  move: string;
}
