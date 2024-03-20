import { getDetailedData } from "@/app/actions/movie-actions"
import MovieImage from "@/app/components/movies/movie-image";
import Duration from "@/app/components/movies/movieDetails/duration";
import Genres from "@/app/components/movies/movieDetails/genres";
import Rating from "@/app/components/movies/movieDetails/rating";
import VideoPlayer from "@/app/components/movies/movieDetails/videoPlayer";

export default async function DetailsPage({ params }: { params: { id: string } }) {
  const movie = await getDetailedData(params.id);
  //console.log(movie);

  return (
    <div className="mt-8 ">
      <div className="flex flex-grow justify-end  mx-auto max-w-full w-[900px] items-center">
        <p className="text-gray-800 font-semibold">Added On: {new Date(movie.uploadDate).toLocaleString()}</p>
      </div>

      <div className="grid md:grid-cols-2 bg-black bg-opacity-15 gap-3  shadow-3xl max-w-4xl mx-auto p-3 rounded-lg  relative">
        <VideoPlayer imageUrl={movie.imageUrl} videoUrl="https://www.youtube.com/watch?v=mqqft2x_Aa4" />

        <div className="ml-2 relative">
          <h2 className="text-3xl font-bold text-third">{movie.title}</h2>
          <div className="mt-2 mb-4">
            <Genres genres={['Action']} />
            <div className="mt-2">
              <Rating rating={movie.rating} />
            </div>
          </div>

          <div className="space-y-3">
            <p className="text-gray-600 font-semibold">{movie.description}</p>

            <p className="text-gray-600">Release Date: {movie.releaseDate != null && new Date(movie.releaseDate).toLocaleDateString()}</p>


            <Duration duration={movie.duration} />
          </div>
        </div>
      </div>
    </div>
  )
}
