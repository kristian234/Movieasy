import { Movie } from "@/types";
import Image from "next/image";
import Link from "next/link";

interface Props{
    movie: Movie
}

export default function MovieCreatedToast({movie} : Props){
    return(
        <Link href={`/movies/details/${movie.id}`} className="flex flex-col items-center">
            <div className="flex flex-row items-center gap-2">
                <Image src={movie.imageUrl} alt="image" height={80} width={80} className="rounded-lg w-auto h-auto"/>
                <span>New Movie! {movie.title}</span>
            </div>
        </Link>
    )
}