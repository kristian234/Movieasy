'use client'

import { Profile } from "@/types";
import { yupResolver } from "@hookform/resolvers/yup";
import { Button, CustomFlowbiteTheme } from "flowbite-react";
import { Fragment, useEffect } from "react";
import { FieldValues, useForm } from "react-hook-form";
import * as yup from 'yup';
import CustomInput from "../shared/custom-input";
import { updateProfile } from "@/app/actions/profile-actions";
import { ToastContainer, toast } from "react-toastify";
import { useRouter } from "next/navigation";

interface Props {
    profile: Profile
    userId: string
}

const validationSchema = yup.object().shape({
    details: yup.string().max(1000, "Max 1000 characters").min(100, "Min 100 characters").nullable(),
});

export default function ProfileForm({ profile, userId }: Props) {
    const router = useRouter();
    const { control, register, handleSubmit, setFocus, reset, getValues, setValue,
        formState: { isSubmitting, isValid, isDirty, errors } } = useForm({
            mode: 'onTouched',
            resolver: yupResolver(validationSchema),
        });

    useEffect(() => {
        console.log(profile);
        reset({
            details: profile.details
        })

        setFocus('details')
    }, [setFocus, reset])

    async function onSubmit(data: FieldValues) {
        data = { ...data, userId }
        let res = await updateProfile(data);

        if ((res as any).error) {
            if ((res as any).error.status === 500) {
                toast.error("Unexpected error occurred. Actor with this name may already exist.")
                return;
            }
        }

        if (res) {
            router.back();
            router.refresh();
        }

    }

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
                        <CustomInput label="Details" name="details" control={control} />


                        <Button isProcessing={isSubmitting} disabled={!isValid}
                            type="submit" theme={customTheme} color="primary">
                            Update
                        </Button>
                    </form>
                </div>
            </div>
        </div>
    )
}