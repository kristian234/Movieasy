'use client'

import { getMovieActors } from '@/app/actions/movie-actions';
import { Actor } from '@/types';
import { useEffect, useState } from 'react';
import Modal from 'react-modal';
import { toast } from 'react-toastify';
import {
    Accordion,
    AccordionItem,
    AccordionItemHeading,
    AccordionItemButton,
    AccordionItemPanel,
} from 'react-accessible-accordion';
import { Spinner } from 'flowbite-react';
import { useParamsStore } from '@/hooks/useParamsStore';
import { useRouter } from 'next/navigation';
//import 'react-accessible-accordion/dist/fancy-example.css';

const customStyles = {
    overlay: {
        backgroundColor: 'rgba(0, 0, 0, 0.5)', // semi-transparent dark background
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
    },
    content: {
        border: 'none', // no border
        borderRadius: '0.5rem', // rounded corners
        boxShadow: '0 0 20px rgba(0, 0, 0, 0.2)', // cooler shadow effect
        maxWidth: '800px', // maximum width of the modal (adjusted width)
        width: '90%', // set the width to 90% of the viewport
        margin: 'auto', // center the modal horizontally
    },
};

interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
    title?: string;
    cancelButtonLabel?: string;
    onCancelButtonClick?: () => void;
    movieId: string
}

export default function ActorsModal({ isOpen, onClose, title, cancelButtonLabel, onCancelButtonClick, movieId }: ModalProps) {
    const [actors, setActors] = useState<Actor[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        if (isOpen) {
            fetchActors();
        }
    }, [isOpen])

    const fetchActors = async () => {
        try {
            setIsLoading(true);
            const res = await getMovieActors(movieId);

            if ((res as any).error) {
                toast.error((res as any).error.message);
                setIsLoading(false);
                return;
            }

            setActors(res);
            setIsLoading(false);
        } catch (error) {
            throw error; // TO DO: might be worth looking into this
        }
    };

    const setParams = useParamsStore(state => state.setParams);
    const router = useRouter();


    function search(actorName: string) {
        setParams({ searchTerm: actorName });
        // over here redirect the mto the search page
        router.push('/movies/search')
    }

    return (
        <Modal
            isOpen={isOpen}
            onRequestClose={onClose}
            className="modal-wrapper"
            style={customStyles}
        >
            <div className="modal-content bg-header rounded-lg shadow-3xl p-6 w-full">
                <div className="modal-header border-b-2 mb-6">
                    {title && <h1 className='text-4xl text-secondary text-center font-semibold'>{title}</h1>}
                </div>
                {isLoading ? (
                    <div className='flex justify-center items-center'>
                        <Spinner />
                    </div>
                ) : (
                    <div className="modal-body overflow-y-auto max-h-80">
                        <Accordion allowZeroExpanded={true} className="accordion">
                            {actors.map((actor) => (
                                <AccordionItem key={actor.id} className='mb-3'>
                                    <AccordionItemHeading className='text-secondary border-b'>
                                        <AccordionItemButton className="text-xl font-bold">
                                            {actor.name}
                                        </AccordionItemButton>
                                    </AccordionItemHeading>
                                    <AccordionItemPanel className="bg-third rounded-lg bg-opacity-5 p-4">
                                        <p className="text-1sm text-third font-semibold">{actor.biography}</p>
                                        <div>
                                            <button
                                                className="btn btn-secondary mt-4 w-full bg-secondary hover:bg-third rounded-md font-semibold"
                                                onClick={() => search(actor.name)}
                                            >
                                                More Movies
                                            </button>
                                        </div>
                                    </AccordionItemPanel>
                                </AccordionItem>
                            ))}
                        </Accordion>
                    </div>
                )}

                <div className="modal-footer flex justify-center flex-row space-x-7 mt-3">
                    {onCancelButtonClick && (
                        <button className="
                                modal-cancel
                                flex w-[50%]
                                justify-center
                                items-center
                                btn btn-secondary mr-2
                                bg-secondary text-primary font-semibold 
                                px-4 py-1 rounded-lg
                                hover:bg-third
                                focus:outline-none" onClick={onCancelButtonClick}>
                            {cancelButtonLabel}
                        </button>
                    )}
                </div>
            </div>
        </Modal>
    )
}