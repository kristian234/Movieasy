import { getDetailedData } from "@/app/actions/movie-actions"
import Image from "next/image";

export default async function DetailsPage({ params }: { params: { id: string } }) {
    const movie = await getDetailedData(params.id);
    console.log(movie);
    return (
        <div className="flex flex-col md:flex-row bg-white shadow-lg rounded-lg max-w-2xl mx-auto">
        <div className="flex items-start justify-between md:w-1/2">
          <Image src={movie.imageUrl} alt={movie.title} width={200} height={300} className="w-full object-cover object-center p-4" />
        </div>
        <div className="w-full md:w-1/2 p-4 mt-4 md:mt-0">
          <h2 className="text-gray-900 font-semibold text-2xl tracking-wide">{movie.title}</h2>
          <p className="text-gray-700 mt-2">{movie.description}</p>
          <p className="mt-2"><span className="font-semibold">Release Date:</span> {movie.releaseDate != null && new Date(movie.releaseDate).toLocaleDateString()}</p>
          <p><span className="font-semibold">Rating:</span> {movie.rating}</p>
          <p><span className="font-semibold">Duration:</span> {movie.duration} minutes</p>
        </div>
      </div>
    )
}
