export interface Movie {
  id: string
  title: string
  description: string
  releaseDate: Date | null
  uploadDate: Date
  rating: MovieRating
  duration: number
  imageUrl: string
  trailerUrl: string
  genres: Genre[]
  actors: Actor[]
}

export interface Actor{
  id: string,
  name: string,
  biography: string
}

export interface Review{
  id: string,
  comment: string,
  reviewerName: string,
  rating: number,
  createdOnDate: Date
}

export interface Statistic{
  total: number
}

export interface DetailedReviewData{
  averageRating: number,
  totalRatings: number
}

export interface Genre {
  id: string,
  name: string
}

export interface Error {
  error: {
    status: number,
    message: string
  }
}

export enum MovieRating {
  G = 'G',
  PG = 'PG',
  PG13 = 'PG13',
  R = 'R',
  NC17 = 'NC17',
}

export interface PagedResult<T> {
  items: T[]
  pageCount: number
  totalPages: number
}
