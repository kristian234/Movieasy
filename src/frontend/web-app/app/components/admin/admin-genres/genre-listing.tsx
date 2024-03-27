'use client'

import { deleteGenre, getGenres } from "@/app/actions/genre-actions";
import { useGenresStore } from "@/hooks/useGenresStore";
import { useEffect, useState } from "react";
import { LoadingSpinner } from "video-react";
import LoadingComponent from "../../shared/loading-component";
import { Spinner } from "flowbite-react";

export default function GenreListing() {
    const genres = useGenresStore((state) => state.genres);
    const setGenres = useGenresStore(state => state.setGenres);
    const delGenre = useGenresStore(state => state.deleteGenre);
    useEffect(() => {
        getGenres().then(result => {
            if ((result as any).error) {
                // TO DO: add a toast

                return;
            }

            setGenres(result);
        });
    }, [])

    async function removeGenre(id: string) {
        const res = await deleteGenre(id);

        if (res.error) {
            // TO DO: add a toast

            return;
        }

        delGenre(id); // Delete from the zustand store
    }

    return (
        <div className="flex justify-center p-6 mt-10">
            <div className="w-full bg-header rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0">
                <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
                    <h1 className="text-xl font-bold leading-tight tracking-tight text-secondary md:text-2xl">
                        Genres
                    </h1>
                    {genres.length === 0 ? (
                        <div className="flex justify-center items-center mt-10">
                            <Spinner />

                        </div>

                    ) : (
                        <ul className="list-none p-0 m-0 overflow-y-auto max-h-80">
                            {genres.map((genre, index) => (
                                <li key={genre.id} className={`flex justify-between items-center p-4 bg-opacity-55 bg-third rounded-lg shadow-md ${index !== 0 ? 'mt-4' : ''}`}> { }
                                    <span className="text-lg font-semibold text-primary">{genre.name}</span>
                                    <div className="flex space-x-2">
                                        <button className="bg-secondary hover:bg-third text-primary px-2 py-1 rounded-md">Edit</button>
                                        <button onClick={() => removeGenre(genre.id)} className="bg-danger hover:bg-superdanger text-primary px-2 py-1 rounded-md">Delete</button>
                                    </div>
                                </li>
                            ))}
                        </ul>
                    )}

                </div>
            </div>
        </div>
    )
}
