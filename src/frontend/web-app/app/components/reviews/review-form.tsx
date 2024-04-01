'use client'
import { createReview } from '@/app/actions/review-actions';
import { Rating } from '@smastrom/react-rating'
import '@smastrom/react-rating/style.css'
import { Button, CustomFlowbiteTheme, Textarea } from 'flowbite-react';
import { useState } from 'react'
import { FieldValues, useForm } from 'react-hook-form';
import { AiOutlineSend } from "react-icons/ai";
import { toast } from 'react-toastify';
import ReviewRating from './review-rating';

const customTheme: CustomFlowbiteTheme['button'] = {
    color: {
        primary: 'bg-secondary hover:bg-third',
    },
};

interface Props {
    movieId: string
}

export default function ReviewForm({ movieId }: Props) {
    const [rating, setRating] = useState(1);
    const { control, register, handleSubmit, setFocus, reset, getValues, setValue,
        formState: { isSubmitting, isValid, isDirty, errors } } = useForm({
            mode: 'onTouched'
        });

    async function onSubmit(data: FieldValues) {
        const finalData = { ...data, rating, movieId }

        const res = await createReview(finalData);

        if ((res as any).error) {
            toast.error((res as any).error.message)

            return;
        }
    }

    return (
        <div className=''>
            <div className="py-1 mt-5 flex items-center text-lg font-semibold text-secondary before:flex-[1_1_0%] before:border-t before:border-third before:me-6 after:flex-[1_1_0%] after:border-t after:border-third after:ms-6">Leave a Review</div>
            <div className='flex flex-col items-end'>
                <div className="p-4 w-full">
                    <form onSubmit={(e) => { handleSubmit(onSubmit)(e); }}>
                        <div className="flex-grow mb-4">
                            <ReviewRating value={rating} onChange={setRating} />
                        </div>
                        <div className="flex items-end h-36">
                            <Textarea
                                {...register('comment', { required: 'A comment is required' })}
                                maxLength={500}
                                required
                                rows={4}
                                className="mt-4 p-2 rounded border w-full font-semibold border-secondary focus:ring-secondary focus:ring-0 focus:ring-offset-0 text-primary bg-header"
                                placeholder="Write your review here..."
                                style={{ resize: 'none', height: '100%' }} // limit the size
                            />
                            <Button isProcessing={isSubmitting} disabled={!isValid}
                                type="submit" theme={customTheme} color="primary" className="ml-2 h-full">
                                <AiOutlineSend />
                            </Button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    )
}