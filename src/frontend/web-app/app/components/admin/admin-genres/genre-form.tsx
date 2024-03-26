'use client'

import { FieldValues, useForm } from "react-hook-form";
import { Button, CustomFlowbiteTheme } from "flowbite-react";
import CustomInput from "../../shared/custom-input";
import { Fragment, useEffect } from "react";
import { createMovie, updateMovie } from "@/app/actions/movie-actions";
import { Genre, Movie } from "@/types";

const MovieRating = {
    G: 1,
    PG: 2,
    PG13: 3,
    R: 4,
    NC17: 5,
} as const;

interface Props {
    title: string
    genre?: Genre
}

export default function GenreForm({ title, genre }: Props) {
    const { control, register, handleSubmit, setFocus, reset,
        formState: { isSubmitting, isValid, isDirty, errors } } = useForm({
            mode: 'onTouched'
        });

    useEffect(() => {
        if (genre) {
            const { name } = genre;
            reset({
                name
            });
        }

        setFocus('name')
    }, [setFocus])

    async function onSubmit(data: FieldValues) {
        const { photo, releaseDate, ...otherData } = data;

        const formData = new FormData();

        // Append the photo to formData
        formData.append('photo', photo[0]);


        Object.keys(otherData).forEach(key => {
            let value = otherData[key];

            formData.append(key, value);
        });

        if (releaseDate) {
            // Get the selected release date
            const selectedDate = new Date(releaseDate);

            // Set the time zone offset to zero (UTC)
            selectedDate.setMinutes(selectedDate.getMinutes() - selectedDate.getTimezoneOffset());

            // Convert to ISO string
            const formattedReleaseDate = selectedDate.toISOString().split('T')[0];

            formData.append('releaseDate', formattedReleaseDate);
        }

        let res;
        if (movie) {
            formData.append('MovieId', movie.id);
            res = await updateMovie(formData);
        } else {
            res = await createMovie(formData);
        }
        console.log(res.error);
        if (res.error) {
            throw new Error(res.error);
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

                        <CustomInput label="Name" name="title" control={control}
                            rules={{ required: 'Make is required' }} />

                        <Button isProcessing={isSubmitting} disabled={!isValid}
                            type="submit" theme={customTheme} color="primary">
                            {name ? (
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