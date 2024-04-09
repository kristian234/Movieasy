'use client'

import { FieldValues, useForm } from "react-hook-form";
import { Button, CustomFlowbiteTheme } from "flowbite-react";
import CustomInput from "../../shared/custom-input";
import { Fragment, useEffect } from "react";
import { Genre } from "@/types";
import { createGenre, getGenres, updateGenre } from "@/app/actions/genre-actions";
import { useGenresStore } from "@/hooks/useGenresStore";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import * as yup from 'yup';
import { yupResolver } from "@hookform/resolvers/yup";

interface Props {
    title: string
    genre?: Genre
}

const validationSchema = yup.object().shape({
    name: yup.string().required('Name is required').max(50, "Max 50 characters"),
});

export default function GenreForm({ title, genre }: Props) {
    const { control, register, handleSubmit, setFocus, reset,
        formState: { isSubmitting, isValid, isDirty, errors } } = useForm({
            resolver: yupResolver(validationSchema),
            mode: 'onTouched'
        });

    const router = useRouter();
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

                return;
            }

            // Edit a genre
            data = { ...data, genreId: genre.id };

            res = await updateGenre(data);

            if ((res as any).error) {
                toast.error(res.error.message);
                return;
            }

            if (!(res as any).error) {
                router.back();
            }
        } catch (error: any) {
            toast.error("Unexpected error");
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

                        <CustomInput label="Name" name="name" control={control} />

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