export interface Report {
  id: number;
  description: string;
  creationTime: Date;
  pgn: string;
}

export interface GenerateReport {
  description: string;
  pgn: string;
}

export interface EditReport {
  description: string;
}
