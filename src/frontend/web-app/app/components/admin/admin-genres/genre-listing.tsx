'use client'

import { deleteGenre, getGenres } from "@/app/actions/genre-actions";
import { useGenresStore } from "@/hooks/useGenresStore";
import { useEffect, useState } from "react";
import { Spinner } from "flowbite-react";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import CustomModal from "../../shared/modal";

export default function GenreListing() {
    const genres = useGenresStore((state) => state.genres);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const setGenres = useGenresStore(state => state.setGenres);
    const [showConfirmation, setShowConfirmation] = useState(false);
    const delGenre = useGenresStore(state => state.deleteGenre);
    const [selectedGenreId, setSelectedGenreId] = useState<string | null>(null);

    const router = useRouter();

    const [isDeleting, setIsDeleting] = useState(false);

    useEffect(() => {
        getGenres().then(result => {
            if ((result as any).error) {
                // TO DO: add a toast
                toast.error((result as any).error);
                setIsLoading(false);
                return;
            }

            setGenres(result);
            setIsLoading(false);
        });
    }, [])

    async function removeGenre(id: string) {
        setIsDeleting(true);
        const res = await deleteGenre(id);

        if (res.error) {
            setIsDeleting(false);
            setShowConfirmation(false);
            toast.error(res.error);
            return;
        }

        delGenre(id); // Delete from the zustand store
        setShowConfirmation(false);
        setIsDeleting(false);
    }

    return (
        <div className="flex justify-center p-6 mt-10">
            <div className="w-full bg-header rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0">
                <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
                    <h1 className="text-xl font-bold leading-tight tracking-tight text-secondary md:text-2xl">
                        Genres
                    </h1>
                    {isLoading ? (
                        <div className="flex justify-center items-center mt-10">
                            <Spinner />
                        </div>
                    ) : genres.length === 0 ? (
                        <div className="flex justify-center items-center mt-10">
                            <h1 className="text-secondary font-semibold">No genres found</h1>
                        </div>
                    ) : (
                        <ul className="list-none p-0 m-0 overflow-y-auto max-h-80">
                            {genres.map((genre, index) => (
                                <li key={genre.id} className={`flex justify-between items-center p-4 bg-opacity-55 bg-third rounded-lg shadow-md ${index !== 0 ? 'mt-4' : ''}`}>
                                    <span className="text-lg font-semibold text-primary">{genre.name}</span>
                                    <div className="flex space-x-2">
                                        <button onClick={() => { router.push(`/admin/genres/edit/${genre.id}`); router.refresh() }} className="bg-secondary hover:bg-third text-primary px-2 py-1 rounded-md">Edit</button>
                                        <button onClick={() => { setSelectedGenreId(genre.id); setShowConfirmation(true) }} className="bg-danger hover:bg-superdanger text-primary px-2 py-1 rounded-md">Delete</button>
                                    </div>
                                </li>
                            ))}
                            <CustomModal
                                isOpen={showConfirmation}
                                onClose={() => setShowConfirmation(false)}
                                title="Confirm Deletion"
                                content="Are you sure you want to delete this movie?"
                                actionButtonLabel="Delete"
                                onActionButtonClick={() => {
                                    if (selectedGenreId) {
                                        removeGenre(selectedGenreId);
                                    }
                                }}
                                cancelButtonLabel="Cancel"
                                onCancelButtonClick={() => setShowConfirmation(false)}
                                loading={isDeleting}
                            />
                        </ul>

                    )}
                </div>
            </div>
        </div>
    )
}
