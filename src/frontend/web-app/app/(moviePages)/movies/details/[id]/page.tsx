import { getCurrentUser } from "@/app/actions/auth-actions";
import { getDetailedData } from "@/app/actions/movie-actions"
import ShowActorsButton from "@/app/components/actors/show-actors-button";
import DeleteButton from "@/app/components/admin/admin-movies/delete-button";
import Duration from "@/app/components/movies/movieDetails/duration";
import Genres from "@/app/components/movies/movieDetails/genres";
import Rating from "@/app/components/movies/movieDetails/rating";
import ReleaseDate from "@/app/components/movies/movieDetails/release-date";
import VideoPlayer from "@/app/components/movies/movieDetails/videoPlayer";
import ReviewForm from "@/app/components/reviews/review-form";
import ShowReviewsButton from "@/app/components/reviews/show-review-button";
import Link from "next/link";
import { notFound } from "next/navigation";
import { Fragment } from "react";

export default async function DetailsPage({ params }: { params: { id: string } }) {
  const movie = await getDetailedData(params.id);
  const session = await getCurrentUser();
  const isAdmin = session?.roles?.includes('Admin') ?? false;

  if ((movie as any).error) {
    return notFound();
  }

  console.log(movie);

  return (
    <div className="mt-8 relative">

      <div className="flex flex-grow justify-start mx-auto max-w-full w-[900px] items-center mb-1">

        {isAdmin && (
          <Fragment>
            <Link href={`/admin/movies/edit/${params.id}`} className="flex flex-grow justify-start px-6 py-1 text-xs font-medium leading-6 text-center text-primary transition bg-secondary rounded shadow ripple hover:shadow-lg hover:bg-third focus:outline-none">
              Edit
            </Link>

            <DeleteButton movieId={params.id} />
          </Fragment>
        )}


        <div className="flex flex-grow justify-end  mx-auto max-w-full w-[900px] items-center ml-3">
          <p className="text-gray-800 font-semibold">Added On: {new Date(movie.uploadDate).toLocaleString()}</p>
        </div>
      </div>


      <div className="grid md:grid-cols-2 bg-black bg-opacity-15 gap-3  shadow-3xl max-w-4xl mx-auto p-3 rounded-lg  relative">
        <VideoPlayer imageUrl={movie.imageUrl} videoUrl={movie.trailerUrl} />

        <div className="ml-2 relative flex flex-col justify-between ">
          <h2 className="text-3xl font-bold text-third">{movie.title}</h2>
          <div className="mt-3 mb-4">
            <Genres genres={movie.genres} />

            <div className="mt-3 flex justify-start md:justify-between">
              <Rating rating={movie.rating} />
              <ReleaseDate date={movie.releaseDate ? new Date(movie.releaseDate) : null} />
              <Duration duration={movie.duration} />
            </div>
          </div>


          <div className="space-y-3 mt-6">
            <p className="text-gray-600 font-semibold">{movie.description}</p>
          </div>

          <div className="mt-3">
            <ShowActorsButton movieId={movie.id} />
          </div>

          <div className="mt-auto">
            <ReviewForm userId={session?.id} movieId={movie.id} />
          </div>
        </div>
      </div>

      <ShowReviewsButton movieId={movie.id} isAdmin={isAdmin}/>
    </div>
  )
}
