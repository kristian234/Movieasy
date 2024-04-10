import { Actor } from "@/types";
import { useEffect, useState } from "react";
import qs from 'query-string'
import { getData } from "@/app/actions/actor-actions";
import { toast } from "react-toastify";
import { MultiSelect } from "react-multi-select-component";
import { Option } from 'react-multi-select-component';
import { IoSendSharp } from "react-icons/io5";

interface Props {
    onSelect: (selectedActors: Actor[]) => void;
}

export default function ActorMultiSelect({ onSelect }: Props) {
    const [actors, setActors] = useState<Actor[]>([]); // Initialize actors state with an empty array
    const [selectedActors, setSelectedActors] = useState<Actor[]>([]);
    const [searchTerm, setSearchTerm] = useState<string>("");
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [page, setPage] = useState<number>(1);
    const [hasMore, setHasMore] = useState<boolean>(true);

    useEffect(() => {
        if (actors.length === 0) {
            fetchData();
        }
    }, [actors]);

    const fetchData = async () => {
        setIsLoading(true);
        try {
            const res = await getData(`?page=${page}&searchTerm=${searchTerm}`);

            if ((res as any).error) {
                toast.error((res as any).error.message);
                return;
            }

            const newActors = res.items.filter((actor) => !actors.some((existingActor) => existingActor.id === actor.id));

            setActors((prevActors) => [...newActors]);
            setHasMore(res.pageCount > page);
        } catch (error) {
            console.error("Error fetching data:", error);
        } finally {
            setIsLoading(false);
        }
    };

    const handleSearch = () => {
        setPage(1);
        setActors([]);
    };

    const handleSelectActor = (actor: Actor) => {
        const isDuplicate = selectedActors.some((selectedActor) => selectedActor.id === actor.id);
        if (!isDuplicate) {
            setSelectedActors([...selectedActors, actor]);
            onSelect([...selectedActors, actor]);
        }
    };

    const handleRemoveActor = (actorId: string) => {
        const updatedActors = selectedActors.filter((actor) => actor.id !== actorId);
        setSelectedActors(updatedActors);
        onSelect(updatedActors);
    };



    return (
        <div className="bg-gray-100 p-4 rounded-md shadow-md">
            <div className="mb-4 flex">
                <input
                    type="text"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    placeholder="Search actors"
                    className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring focus:border-blue-500 mr-2"
                />
                <button type="button" onClick={handleSearch} className="px-2 py-2 bg-blue-500 text-white rounded-md"><IoSendSharp /></button>
            </div>
            <div className="overflow-y-auto max-h-36" style={{ maxHeight: "200px" }}>
                {actors.map((actor) => (
                    <div key={actor.id} className="flex justify-between items-center px-4 py-2 border-b border-t border-gray-400">
                        <span>{actor.name}</span>
                        <button type="button" onClick={() => handleSelectActor(actor)} className="px-3 py-1 bg-blue-500 text-white rounded-md">Add</button>
                    </div>
                ))}
                {!isLoading && hasMore && (
                    <button type="button" onClick={() => setPage(page + 1)} className="block w-full px-4 py-2 bg-blue-500 text-white rounded-md">Load More</button>
                )}
            </div>
            <div>
                <span className="block mb-2">Selected Actors:</span>
                <div className="overflow-y-auto max-h-36" style={{ maxHeight: "200px" }}>
                    <ul>
                        {selectedActors.map((actor) => (
                            <li key={actor.id} className="flex justify-between items-center px-4 py-2 border-b border-gray-200">
                                <span>{actor.name}</span>
                                <button type="button" onClick={() => handleRemoveActor(actor.id)} className="px-3 py-1 bg-red-500 text-white rounded-md">Remove</button>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        </div>
    );
}