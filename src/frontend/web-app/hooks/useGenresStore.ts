import { Genre } from "@/types";
import { create } from "zustand";

type State = {
    genres: Genre[];
};

type Actions = {
    setGenres: (newGenres: Genre[]) => void;
    addGenre: (genre: Genre) => void;
    deleteGenre: (id: string) => void;
    resetGenres: () => void;
};

const initialGenres: Genre[] = [];

// Zustand store for genres
export const useGenresStore = create<State & Actions>((set) => ({
    genres: initialGenres,

    setGenres: (newGenres: Genre[]) => {
        set({ genres: newGenres });
    },

    addGenre: (newGenre: Genre) => {
        set((state) => ({
            genres: [...state.genres, newGenre],
        }));
    },
    
    resetGenres: () => {
        set({ genres: initialGenres });
    },

    deleteGenre: (id: string) => {
        set(state => ({
            genres: state.genres.filter(genre => genre.id !== id)
        }));
    },
}));
