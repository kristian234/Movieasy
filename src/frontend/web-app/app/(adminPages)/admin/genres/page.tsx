import GenreForm from "@/app/components/admin/admin-genres/genre-form";
import GenreListing from "@/app/components/admin/admin-genres/genre-listing";

export default function GenresPage() {
    return (
        <div className="overflow-x-hidden">
            <div className="flex flex-col md:flex-row">

                <div className="md:w-1/2  justify-center">
                    <div className="">
                        <GenreForm title="Create Genre" />
                    </div>
                </div>
                <div className="md:w-1/2 justify-center">
                    <div className="">
                        <GenreListing />
                    </div>
                </div>
                
            </div>
        </div>
    );
}
