'use client'

interface Props{
    genres: string[]
}

export default function Genres({genres} : Props) {
    return (
        <div className="flex flex-wrap">
            {genres.map((genre, index) => (
                <span key={index} className="bg-secondary text-primary rounded-full px-3 py-1 mr-2 mb-1 text-sm font-semibold">
                    {genre}
                </span>
            ))}
        </div>
    )
}