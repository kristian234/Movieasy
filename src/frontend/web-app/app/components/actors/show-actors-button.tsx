'use client'

import { useState } from "react"
import ActorsModal from "./actors-modal";

interface Props {
    movieId: string
}

export default function ShowActorsButton({ movieId }: Props) {
    const [showModal, setShowModal] = useState<boolean>(false);

    function onCancelButtonClick() {
        setShowModal(false);
    }

    return (
        <div>
            <button onClick={() => setShowModal(true)} className="flex items-center max-w-3xl justify-center mt-3 mx-auto text-secondary py-2 px-4 rounded-md shadow-sm text-sm font-semibold w-full bg-header hover:bg-darkHeader focus:outline-none focus:ring-1 border border-secondary focus:ring-secondary">
                Show Cast
            </button>

            <ActorsModal movieId={movieId} isOpen={showModal} title="Actors" cancelButtonLabel="Close" onClose={onCancelButtonClick} onCancelButtonClick={onCancelButtonClick}/>
        </div>
    )
}