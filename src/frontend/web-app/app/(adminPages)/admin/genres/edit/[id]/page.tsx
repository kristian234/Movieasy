import { getGenre } from "@/app/actions/genre-actions";
import GenreForm from "@/app/components/admin/admin-genres/genre-form";

export default async function GenreEditPage({ params }: { params: { id: string } }) {
    const genre = await getGenre(params.id);

    return (
        <div>
            <GenreForm genre={genre} title="Update Genre"></GenreForm>
        </div>
    )
}