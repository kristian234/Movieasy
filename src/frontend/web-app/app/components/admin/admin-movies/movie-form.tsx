'use client'

import { FieldValues, useForm } from "react-hook-form";
import { Button, CustomFlowbiteTheme, Dropdown, FileInput, TextInput } from "flowbite-react";
import CustomInput from "../../shared/custom-input";
import { Fragment, useEffect, useState } from "react";
import DateInput from "../../shared/date-input";
import { createMovie, updateMovie } from "@/app/actions/movie-actions";
import { Movie } from "@/types";
import { MultiSelect } from "react-multi-select-component";
import { useGenresStore } from "@/hooks/useGenresStore";
import { getGenres } from "@/app/actions/genre-actions";

const MovieRating = {
    G: 1,
    PG: 2,
    PG13: 3,
    R: 4,
    NC17: 5,
} as const;

interface Props {
    title: string
    movie?: Movie
}

export default function MovieForm({ title, movie }: Props) {
    const { control, register, handleSubmit, setFocus, reset, getValues, setValue,
        formState: { isSubmitting, isValid, isDirty, errors } } = useForm({
            mode: 'onTouched'
        });

    const genres = useGenresStore(state => state.genres)
    const setGenres = useGenresStore(state => state.setGenres)
    const [selectedGenres, setSelectedGenres] = useState([])


    useEffect(() => {
        getGenres().then(result => {
            if ((result as any).error) {
                // TO DO: add a toast

                return;
            }

            setGenres(result);
        });
    }, [])

    const options = genres.map(genre => ({ label: genre.name, value: genre.id }))


    useEffect(() => {
        if (movie) {
            const { title, description, releaseDate, duration, rating, imageUrl } = movie;
            const photo = imageUrl;

            reset({
                title, description, releaseDate,
                duration: Math.round((duration + Number.EPSILON) * 100) / 100,
                rating: MovieRating[rating], photo
            });
        }

        setFocus('title')
    }, [setFocus, reset])

    async function onSubmit(data: FieldValues) {
        const { photo, releaseDate, ...otherData } = data;

        const formData = new FormData();

        // Append the photo to formData
        formData.append('photo', photo[0]);


        Object.keys(otherData).forEach(key => {
            let value = otherData[key];

            formData.append(key, value);
        });

        
        selectedGenres.forEach((genre: { label: string, value: string }) => {
            formData.append('genres', genre.value);
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
            console.log(data);
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

                        <CustomInput label="Title" name="title" control={control}
                            rules={{ required: 'Make is required' }} />
                        <CustomInput label="Description" name="description" control={control}
                            rules={{ required: 'Description is required' }} />
                        <DateInput dateFormat={'yyyy-MM-dd'} label="Release Date" name="releaseDate" type="date"
                            control={control} />

                        <div className="flex flex-row justify-between">
                            <CustomInput label="Duration" name="duration" control={control} type="number" rules={{ required: 'Duration is required' }}></CustomInput>
                            <div>
                                <select {...register('rating', { required: 'Rating is required' })} id="rating" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg w-18 p-2.5">
                                    {Object.entries(MovieRating).map(([rating, value]) => (
                                        <option key={value} value={value}>{rating}</option>
                                    ))}
                                </select>
                            </div>
                        </div>


                        <FileInput {...register('photo')} required={movie == null} name="photo" />

                        {movie && (
                            <p className="text-primary font-semibold">This movie currently has a photo, leave to none to not change</p>
                        )}

                        <MultiSelect
                            options={options}
                            value={selectedGenres}
                            onChange={setSelectedGenres}
                            labelledBy="Select Genres"
                        />

                        <Button isProcessing={isSubmitting} disabled={!isValid}
                            type="submit" theme={customTheme} color="primary">
                            {movie ? (
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