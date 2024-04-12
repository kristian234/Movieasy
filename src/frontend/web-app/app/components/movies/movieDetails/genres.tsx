'use client'

import { useParamsStore } from "@/hooks/useParamsStore";
import { Genre } from "@/types"
import Link from "next/link"
import { useRouter } from "next/navigation";

interface Props {
    genres: Genre[]
}

export default function Genres({ genres }: Props) {
    const setParams = useParamsStore(state => state.setParams);
    const router = useRouter();


    function search(genreName: string) {
        setParams({ searchTerm: genreName });
        // over here redirect the mto the search page
        router.push('/movies/search')
    }
    
    return (
        <div className="flex flex-wrap">
            {genres.map((genre, index) => (
                <span key={index} className="bg-secondary text-primary rounded-full px-3 py-1 mr-2 mb-1 text-sm font-semibold">
                    <button onClick={() => search(genre.name)}>
                        {genre.name}
                    </button>
                </span>
            ))}
        </div>
    )
}