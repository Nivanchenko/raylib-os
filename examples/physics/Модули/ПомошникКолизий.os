
// shouldCollide(objA, objB) {
// 		if (objA.isStatic && objB.isStatic) return false;
// 		if ((objA.collisionMask & objB.collisionMask) === 0) return false;
// 		if ((objA.collisionMaskIgnore & objB.collisionMask) === objB.collisionMask) return false;
// 		if ((objB.collisionMaskIgnore & objA.collisionMask) === objA.collisionMask) return false;
// 		return true;
// 	}