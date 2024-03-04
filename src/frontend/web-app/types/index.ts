export interface Movie {
    title: string
    description: string
    releaseDate: Date | null
    rating: string
    duration: number
  }

  export interface PagedResult<T>{
    results: T[]
    pageCount: number
    totalCount: number
  }
  