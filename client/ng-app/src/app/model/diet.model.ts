export interface DataInputModel {
    currentWeight:number,
    goalWeight:number,
    programDuration:number,
    fatPercentage: number,
    healthIssues: String[],
    junkFoodFrequency: String,
    dailyCalBurn: number
}

export interface MealModel{
    id: number,
    kCal: number,
    carbonHydrates: number,
    proteins: number,
    fats: number,
    name: String,
    mealType: String,
    junkPercentage: number,
    recipe: String,
    healthIssueTypes?: String[],
}

export interface DietModel{
    kCal: number,
    carbonHydrates: number,
    proteins: number,
    fats: number,
    meals: MealModel[]
}

