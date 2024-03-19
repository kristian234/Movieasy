import React from "react";
import MovieImage from "./movie-image";
import Link from "next/link";
import { Movie } from "@/types";

interface Props {
    movie: Movie
    isCarousel: boolean
}

export default function MovieCard({ movie, isCarousel = false }: Props) {
    return (
        <Link href={`/movies/details/${movie.id}`}>
            <div className={`${isCarousel ? "ssm:w-full" : "ssm:w-48"} relative mb-2 p-3 group transition-transform duration-100 hover:scale-95`}>
                <div className="h-0 pb-[125%] relative">
                    <MovieImage src={movie.imageUrl} />
                </div>
                <h1 className="mb-1 line-clamp-2 text-ellipsis break-words font-bold text-primary">{movie.title}</h1>

                {!isCarousel && (
                    <p className="font-semibold text-third text-sm">
                        Movie<span className="mx-[0.6em] text-[1em]">●</span>2011
                    </p>
                )}
            </div>
        </Link>
    );
}
