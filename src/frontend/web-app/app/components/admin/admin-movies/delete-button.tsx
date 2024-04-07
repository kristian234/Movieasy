'use client'

import { deleteMovie } from "@/app/actions/movie-actions";
import { useRouter } from "next/navigation";
import { useState } from "react";
import CustomModal from "../../shared/modal";


interface Props {
    movieId: string;
}

export default function DeleteButton({ movieId }: Props) {
    const router = useRouter();
    const [showConfirmation, setShowConfirmation] = useState(false);
    const [loading, setLoading] = useState(false); 

    async function onDeleteConfirmed() {
        setLoading(true); 
        const res = await deleteMovie(movieId);

        if (res.error) {
            throw res.error;
        }

        router.push('/');
    }

    return (
        <div>
            <button onClick={() => setShowConfirmation(true)} className="flex flex-grow ml-3 justify-start px-6 py-1 text-xs font-medium leading-6 text-center text-white transition bg-red-600 rounded shadow ripple hover:shadow-lg hover:bg-red-700 focus:outline-none">
                Delete
            </button>

            <CustomModal
                isOpen={showConfirmation}
                onClose={() => setShowConfirmation(false)} 
                title="Confirm Deletion"
                content="Are you sure you want to delete this movie?"
                actionButtonLabel="Delete"
                onActionButtonClick={onDeleteConfirmed} 
                cancelButtonLabel="Cancel"
                onCancelButtonClick={() => setShowConfirmation(false)}
                loading={loading}
            />
        </div>
    );
}
