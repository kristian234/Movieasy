'use client'

import { FieldValues, useForm } from "react-hook-form";
import { Button, CustomFlowbiteTheme } from "flowbite-react";
import CustomInput from "../../shared/custom-input";
import { Fragment, useEffect } from "react";
import { Actor, Movie } from "@/types";
import * as yup from 'yup';
import { yupResolver } from "@hookform/resolvers/yup";
import { createActor, updateActor } from "@/app/actions/actor-actions";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";

interface Props {
    title: string
    actor?: Actor
}

const validationSchema = yup.object().shape({
    name: yup.string().required('Name is required').max(160, "Max 160 characters"),
    biography: yup.string().max(1000, "Max 1000 characters"),
});

export default function MovieForm({ title, actor }: Props) {
    const { control, register, handleSubmit, setFocus, reset, getValues, setValue,
        formState: { isSubmitting, isValid, isDirty, errors } } = useForm({
            mode: 'onTouched',
            resolver: yupResolver(validationSchema),
        });

    const router = useRouter();

    useEffect(() => {
        setFocus('name')
    }, [setFocus, reset])

    async function onSubmit(data: FieldValues) {
        try {
            let res;
            
            if (!actor) {
                res = await createActor(data);
            } else if (actor) {
                // Edit an actor
                data = { ...data, genreId: actor.id };

                res = await updateActor(data);
            }

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
                        <CustomInput label="Biography" name="biography" control={control} />

                        <Button isProcessing={isSubmitting} disabled={!isValid}
                            type="submit" theme={customTheme} color="primary">
                            {actor ? (
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