export interface Movie {
    id: string
    title: string
    description: string
    releaseDate: Date | null
    uploadDate: Date
    rating: MovieRating
    duration: number
    imageUrl: string
  }

export interface Genre{
  id: string,
  name: string
}

  export interface Error{
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

  export interface PagedResult<T>{
    items: T[]
    pageCount: number
    totalPages: number
  }
  