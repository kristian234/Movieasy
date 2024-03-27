'use client'

import { FieldValues, useForm } from "react-hook-form";
import { Button, CustomFlowbiteTheme } from "flowbite-react";
import CustomInput from "../../shared/custom-input";
import { Fragment, useEffect } from "react";
import { createMovie, updateMovie } from "@/app/actions/movie-actions";
import { Genre } from "@/types";
import { createGenre, getGenres } from "@/app/actions/genre-actions";
import { useGenresStore } from "@/hooks/useGenresStore";

interface Props {
    title: string
    genre?: Genre
}

export default function GenreForm({ title, genre }: Props) {
    const { control, register, handleSubmit, setFocus, reset,
        formState: { isSubmitting, isValid, isDirty, errors } } = useForm({
            mode: 'onTouched'
        });

    const setGenres = useGenresStore((state) => state.setGenres);

    useEffect(() => {
        if (genre) {
            const { name } = genre;
            reset({
                name
            });
        }

        setFocus('name')
    }, [setFocus, setGenres])

    async function onSubmit(data: FieldValues) {
        try {
            let res;

            if (!genre) { // No genre has been passed in, automatically decide its to CREATE an auction POST
                res = await createGenre(data);

                if (!res.error) {
                    const genres = await getGenres();

                    setGenres(genres);
                }
            }

            // TO DO: Add a way to edit a genre.

        } catch (error: any) {
            // TO DO: Add a toast notification that the submission failed.
        }
    }

    const customTheme: CustomFlowbiteTheme['button'] = {
        color: {
            primary: 'bg-secondary hover:bg-third',
        },
    };

    return (
        <div className="flex justify-center p-6 mt-10">
            <div className="w-full bg-header rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0">
                <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
                    <h1 className="text-xl font-bold leading-tight tracking-tight text-secondary md:text-2xl">
                        {title}
                    </h1>
                    <form onSubmit={(e) => { handleSubmit(onSubmit)(e); }} className="space-y-4 md:space-y-6" action="#">

                        <CustomInput label="Name" name="name" control={control}
                            rules={{ required: 'Name is required' }} />

                        <Button isProcessing={isSubmitting} disabled={!isValid}
                            type="submit" theme={customTheme} color="primary">
                            {genre ? (
                                <Fragment>
                                    Update
                                </Fragment>
                            ) : (
                                <Fragment>
                                    Create
                                </Fragment>
                            )}
                        </Button>
                    </form>
                </div>
            </div>
        </div>
    )
}