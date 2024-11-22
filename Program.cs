//*****************************************************************************
//** 1072. Flip Columns For Maximum Number of Equal Rows    leetcode         **
//*****************************************************************************

#define MAX_ROWS 1000
#define MAX_COLS 1000

int HashTable[MAX_ROWS] = {0};
int keyStorage[MAX_ROWS][MAX_COLS] = {{0}};
int keySizes[MAX_ROWS] = {0};
int hashSize = 0;

int compareKeys(const int *key1, int size1, const int *key2, int size2) {
    if (size1 != size2) {
        return 0;
    }
    return memcmp(key1, key2, size1 * sizeof(int)) == 0;
}

int findOrInsertKey(const int *key, int keySize) {
    for (int i = 0; i < hashSize; ++i) {
        if (compareKeys(keyStorage[i], keySizes[i], key, keySize)) {
            return i; // Key found
        }
    }
    memcpy(keyStorage[hashSize], key, keySize * sizeof(int));
    keySizes[hashSize] = keySize;
    return hashSize++; 
}

int maxEqualRowsAfterFlips(int** matrix, int matrixSize, int* matrixColSize) {
    memset(HashTable, 0, sizeof(HashTable));
    hashSize = 0;

    int N = *matrixColSize;

    for (int i = 0; i < matrixSize; ++i) {
        int key[MAX_COLS];
        for (int j = 0; j < N; ++j) {
            key[j] = matrix[i][j] ^ matrix[i][0]; 
        }
        int index = findOrInsertKey(key, N);
        HashTable[index]++;
    }

    int maxCount = 0;
    for (int i = 0; i < hashSize; ++i) {
        if (HashTable[i] > maxCount) {
            maxCount = HashTable[i];
        }
    }

    return maxCount;
}