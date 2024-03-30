import GenreForm from "@/app/components/admin/admin-genres/genre-form";

export default async function GenreEditPage() {
    const movie = await (params.id);

    return (
        <div>
            <GenreForm genre={movie} title="Update Movie"></GenreForm>
        </div>
    )
}