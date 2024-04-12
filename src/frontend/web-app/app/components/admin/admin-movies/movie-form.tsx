'use client'

import { FieldValues, useForm } from "react-hook-form";
import { Button, CustomFlowbiteTheme, Dropdown, FileInput, TextInput } from "flowbite-react";
import CustomInput from "../../shared/custom-input";
import { Fragment, useEffect, useState } from "react";
import DateInput from "../../shared/date-input";
import { createMovie, getMovieActors, updateMovie } from "@/app/actions/movie-actions";
import { Actor, Movie } from "@/types";
import { MultiSelect } from "react-multi-select-component";
import { useGenresStore } from "@/hooks/useGenresStore";
import { getGenres } from "@/app/actions/genre-actions";
import { toast } from "react-toastify";
import * as yup from 'yup';
import { yupResolver } from "@hookform/resolvers/yup";
import ActorMultiSelect from "../admin-actors/actors-multiselect";
import { getData } from "@/app/actions/actor-actions";

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

const validationSchema = yup.object().shape({
    title: yup.string().required('Title is required').max(200, "Max 200 characters"),
    description: yup.string().required('Description is required').max(1000, "Max 1000 characters"),
    releaseDate: yup.date().nullable(),
    duration: yup.number().required('Duration is required').positive('Duration must be a positive number').typeError("Duration must be a valid number"),
    rating: yup.number().required('Rating is required'),
    photo: yup.mixed().required('Photo is required'),
    trailerUrl: yup.string().required('Trailer URL is required').max(500, "Max 500 characters"),
});

export default function MovieForm({ title, movie }: Props) {
    const { control, register, handleSubmit, setFocus, reset, getValues, setValue,
        formState: { isSubmitting, isValid, isDirty, errors } } = useForm({
            mode: 'onTouched',
            resolver: yupResolver(validationSchema),
        });

    // TO DO: consider directly fetching and not using the zustand store

    const genres = useGenresStore(state => state.genres)
    const setGenres = useGenresStore(state => state.setGenres)

    const [initialActors, setInitialActors] = useState<Actor[]>();
    const [selectedActors, setSelectedActors] = useState<Actor[]>([]);
    const [selectedGenres, setSelectedGenres] = useState([])


    useEffect(() => {
        if (movie) {
            const { title, description, releaseDate, duration, rating, imageUrl, trailerUrl } = movie;
            const photo = imageUrl;

            const selectedGenres = movie.genres.map(genre => ({ value: genre.id, label: genre.name }));
            setSelectedGenres(selectedGenres as any);

            getMovieActors(movie.id)
            .then(res => {
                if((res as any).error){
                    toast.error((res as any).error.message)
                    return;
                }

                setInitialActors(res);
            })
            .catch(err => {
                toast.error(err.message);
            });

            reset({
                title, description, releaseDate, trailerUrl,
                duration: Math.round((duration + Number.EPSILON) * 100) / 100,
                rating: MovieRating[rating], photo
            });
        }

        getGenres().then(result => {
            if ((result as any).error) {
                toast.error((result as any).error)
                return;
            }
            setGenres(result);
        });

        setFocus('title')
    }, [setFocus, reset, setGenres, setSelectedGenres, setInitialActors])

    const options = genres.map(genre => ({ label: genre.name, value: genre.id }))


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

        selectedActors.forEach(actor => {
            formData.append('actors', actor.id); 
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

        if (selectedActors.length === 0) {
            toast.error('A movie must always have at least one actor.');
            return; 
        }
    
        if (selectedGenres.length === 0) {
            toast.error('A movie must always have at least one category.');
            return; 
        }

        let res;
        if (movie) {
            formData.append('MovieId', movie.id);
            res = await updateMovie(formData);
            if (!res.error) {
                // successfully updated movie
                toast.success('Movie updated successfully!');
            }
        } else {
            res = await createMovie(formData);
            if (!res.error) {
                // successfully created movie
                toast.success('Movie created successfully!');
            }
        }

        if (res.error) {
            // Error occurred
            console.log(res.error);
            toast.error(res.error.message);
        }
    }

   const handleSelectActors = (selectedActors : Actor[]) => {
        setSelectedActors(selectedActors);
    };

    const customTheme: CustomFlowbiteTheme['button'] = {
        color: {
            primary: 'bg-secondary hover:bg-third',
        },
    };

    return (
        <div className="flex justify-center p-6 mt-5">
            <div className="w-full bg-header rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0">
                <div className="p-6 space-y-4 md:space-y-6 sm:p-8"> 
                    <form onSubmit={(e) => { handleSubmit(onSubmit)(e); }} className="space-y-4 md:space-y-6" action="#">

                        <CustomInput label="Title" name="title" control={control}/>
                        <CustomInput label="Description" name="description" control={control}/>
                        <DateInput autoComplete="off" dateFormat={'yyyy-MM-dd'} label="Release Date" name="releaseDate" type="date"
                            control={control} />

                        <div className="flex flex-row justify-between">
                            <CustomInput label="Duration" name="duration" control={control} type="number"></CustomInput>
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

                        <CustomInput label="Trailer URL" name="trailerUrl" control={control}/>

                        <MultiSelect                            
                            options={options}
                            value={selectedGenres}
                            hasSelectAll={false}
                            onChange={setSelectedGenres}
                            labelledBy="Select Genres"
                        />

                        <ActorMultiSelect baselineActors={initialActors} onSelect={handleSelectActors} />

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