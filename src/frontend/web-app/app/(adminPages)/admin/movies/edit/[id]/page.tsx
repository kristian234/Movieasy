import { getDetailedData } from "@/app/actions/movie-actions";
import MovieForm from "@/app/components/admin/admin-movies/movie-form";

export default async function MovieEditPage({ params }: { params: { id: string } }) {
    const movie = await getDetailedData(params.id);

    return (
        <div>
            <MovieForm movie={movie} title="Update Movie"></MovieForm>
        </div>
    )
}