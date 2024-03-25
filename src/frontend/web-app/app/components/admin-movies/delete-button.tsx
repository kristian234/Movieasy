'use client'

import { deleteMovie } from "@/app/actions/movie-actions"
import { useRouter } from "next/navigation";

interface Props{
    movieId: string
}

export default function DeleteButton({movieId} : Props) {
    const router = useRouter();

    async function OnClick(){
        const res = await deleteMovie(movieId)

        if(res.error){
            throw res.error;
        }

        router.push('/');
    }

    return (
        <button onClick={() => { OnClick() }} className="flex flex-grow ml-3 justify-start px-6 py-1 text-xs font-medium leading-6 text-center text-primary transition bg-danger rounded shadow ripple hover:shadow-lg hover:bg-superdanger focus:outline-none">
            Delete
        </button>
    )
}