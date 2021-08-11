export interface TrainingModel {
    id: number,
    sessions: SessionModel[],
    restTime: number,
    numberOfSession: number,
    muscles: String[]
}

export interface SessionModel {
    id: number,
    exerciseDTO: ExerciseModel,
    weight: number,
    repetitions: number,
    duration: number
}

export interface ExerciseModel {
    id: number,
    name: String,
    description: String,
    difficulty: String,
    exerciseCategory: String,
    muscleList: String[],
    equipment?: boolean
}

export interface InputDataTraining {
    currentWeight:number,
    goalWeight:number,
    programDuration:number,
    difficulty: String,
    injuries: String[],
    equipment: boolean
}