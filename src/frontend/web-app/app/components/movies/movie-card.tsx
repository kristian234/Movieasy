import React from "react";
import MovieImage from "./movie-image";

interface Props {
    title: string,
    description: string,
    imageUrl: string,
}

export default function MovieCard({ title, description, imageUrl }: Props) {
    return (
        <div className="ssm:w-48 relative mb-2 p-3 group transition-transform duration-100 hover:scale-95">
            <div className="h-0 pb-[125%] relative">
                <MovieImage src={imageUrl} />
            </div>
            <h1 className="mb-1 line-clamp-2 text-ellipsis break-words font-bold text-primary">{title}</h1>
            <p className="font-semibold text-third text-sm">
                Movie<span className="mx-[0.6em] text-[1em]">●</span>2011
            </p>
        </div>
    );
}
