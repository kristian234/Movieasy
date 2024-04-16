import { Movie } from "@/types";
import Image from "next/image";
import Link from "next/link";

interface Props {
    movie: Movie
}

export default function MovieCreatedToast({ movie }: Props) {
    return (
        <Link href={`/movies/details/${movie.id}`}>
            <div className="flex items-center justify-between px-4 py-2 rounded-lg bg-secondary hover:bg-third transition duration-300 ease-in-out shadow-md">
                <div className="flex flex-col items-start">
                    <span className="text-header font-semibold mb-2">New Movie!</span>
                    <div className="flex items-center space-x-4">
                        <div className="relative h-20 w-20">
                            <Image
                                src={movie.imageUrl}
                                alt="Movie Poster"
                                layout="fill"
                                objectFit="contain"
                                className="rounded-lg"
                            />
                        </div>
                        <div className="flex flex-col">
                            <span className="font-semibold text-lg text-header">{movie.title}</span>
                            <span className="text-darkHeader">Released: {movie.releaseDate != null && new Date(movie.releaseDate).toLocaleDateString() || 'TBA'}</span>
                        </div>
                    </div>
                </div>
            </div>
        </Link>
    )
}