export interface Movie {
    title: string
    description: string
    releaseDate: Date | null
    addedOn: Date
    rating: string
    duration: number
  }

  export interface PagedResult<T>{
    items: T[]
    pageCount: number
    totalPages: number
  }
  