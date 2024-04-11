'use client'

import { deleteActor, getData } from "@/app/actions/actor-actions";
import { Actor } from "@/types";
import { Spinner } from "flowbite-react";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { IoSendSharp } from "react-icons/io5";
import CustomModal from "../../shared/modal";
import { toast } from "react-toastify";
import { shallow } from "zustand/shallow";

export default function ActorListing() {
    const [searchTerm, setSearchTerm] = useState('');
    const [actors, setActors] = useState<Actor[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [hasMore, setHasMore] = useState(true);
    const [page, setPage] = useState(1);
    const [showConfirmation, setShowConfirmation] = useState(false);
    const [isDeleting, setIsDeleting] = useState(false);
    const [selectedActorId, setSelectedActorId] = useState<string | null>(null);

    const router = useRouter();

    useEffect(() => {
        fetchActors();
    }, [page]);

    const fetchActors = async () => {
        setIsLoading(true);
        try {
            const response = await getData(`?pageNumber=${page}&searchTerm=${searchTerm}`);
            const newActors = response.items;
            console.log("Fetched actors:", newActors); // Add logging here

            if (page === 1) {
                setActors([...newActors]);
            } else {
                setActors((prevActors) => [...prevActors, ...newActors]);
            }
            setHasMore(response.totalPages > page);
        } catch (error) {
            console.error("Error fetching data:", error);
        } finally {
            setIsLoading(false);
        }
    };

    //const resetSearch = async () => {
    //    setActors([]); 
    //    setPage(1); 

    //      await fetchActors();
    //  };

    const handleSearch = async () => {
        setActors([]);
        setPage(1);
        await fetchActors();
    };

    const handleLoadMore = () => {
        setPage(page + 1);
    };

    const handleDeleteActor = async (id: string) => {
        setIsDeleting(true);
        console.log(id);
        const res = await deleteActor(id);

        if (res.error) {
            setIsDeleting(false);
            setShowConfirmation(false);
            toast.error(res.error.message);
            return;
        }
        
        setActors(prevActors => prevActors.filter(actor => actor.id !== id));

        setShowConfirmation(false);
        setIsDeleting(false);
    };

    return (
        <div className="flex justify-center p-6 mt-10">
            <div className="bg-darkHeader w-full p-4 rounded-md shadow-md">
                <div className="mb-4 flex">
                    <input
                        type="text"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        placeholder="Search actors"
                        className="w-full px-4 py-2 border border-third rounded-md  focus:outline-none focus:ring focus:border-third mr-2 text-sm md:text-base"
                    />
                    <button type="button" onClick={handleSearch} className="px-2 py-2 bg-secondary hover:bg-third text-white rounded-md"><IoSendSharp /></button>
                </div>

                <div className="justify-center items-center flex">
                    {isLoading && <Spinner />}
                </div>

                <div className="overflow-y-auto max-h-36" style={{ maxHeight: "250px" }}>
                    {actors.map((actor, index) => (
                        <div key={actor.id} className="flex justify-between items-center px-4 py-2 border-b border-t border-third">
                            <span className="text-third font-bold text-sm md:text-base">{actor.name}</span>
                            <div className="ml-2 justify-end flex">
                                <button type="button" onClick={() => { router.push(`/admin/actors/edit/${actor.id}`); router.refresh()}} className="px-3 py-1 bg-secondary hover:bg-third  font-semibold text-white rounded-md mr-2 md:mr-1 md:px-2 md:py-1">Edit</button>
                                <button type="button" onClick={() => { setSelectedActorId(actor.id); setShowConfirmation(true) }} className="px-3 py-1 bg-danger hover:bg-superdanger font-semibold text-white rounded-md md:px-2 md:py-1">Delete</button>
                            </div>
                        </div>
                    ))}
                    <CustomModal
                        isOpen={showConfirmation}
                        onClose={() => setShowConfirmation(false)}
                        title="Confirm Deletion"
                        content="Are you sure you want to delete this actor?"
                        actionButtonLabel="Delete"
                        onActionButtonClick={() => {
                            if (selectedActorId) {
                                handleDeleteActor(selectedActorId);
                            }
                        }} 
                        cancelButtonLabel="Cancel"
                        onCancelButtonClick={() => setShowConfirmation(false)}
                        loading={isDeleting}
                    />

                    {!isLoading && hasMore && (
                        <button type="button" onClick={() => handleLoadMore()} className="block w-full px-4 py-2 bg-secondary text-white font-semibold hover:bg-third rounded-md">Load More</button>
                    )}
                </div>
            </div>
        </div>
    );
}