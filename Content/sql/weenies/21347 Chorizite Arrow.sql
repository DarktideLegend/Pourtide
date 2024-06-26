DELETE FROM `weenie` WHERE `class_Id` = 21347;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (21347, 'arrowchorizite', 5, '2024-04-21 09:39:09') /* Ammunition */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (21347,   1,        256) /* ItemType - MissileWeapon */
     , (21347,   3,         82) /* PaletteTemplate - PinkPurple */
     , (21347,   5,          0) /* EncumbranceVal */
     , (21347,   8,          2) /* Mass */
     , (21347,   9,    8388608) /* ValidLocations - MissileAmmo */
     , (21347,  11,       1000) /* MaxStackSize */
     , (21347,  12,          1) /* StackSize */
     , (21347,  13,          0) /* StackUnitEncumbrance */
     , (21347,  14,          2) /* StackUnitMass */
     , (21347,  15,          2) /* StackUnitValue */
     , (21347,  16,          1) /* ItemUseable - No */
     , (21347,  19,          2) /* Value */
     , (21347,  36,       9999) /* ResistMagic */
     , (21347,  44,         30) /* Damage */
     , (21347,  45,          2) /* DamageType - Pierce */
     , (21347,  50,         64) /* AmmoType - ArrowChorizite */
     , (21347,  51,          3) /* CombatUse - Ammo */
     , (21347,  93,     132116) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity, Inelastic */
     , (21347, 150,        103) /* HookPlacement - Hook */
     , (21347, 151,          2) /* HookType - Wall */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (21347,  17, True ) /* Inelastic */
     , (21347,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (21347,  22, 0.30000001192092896) /* DamageVariance */
     , (21347,  29,       1) /* WeaponDefense */
     , (21347,  39, 1.100000023841858) /* DefaultScale */
     , (21347,  62,       1) /* WeaponOffense */
     , (21347,  76,     0.5) /* Translucency */
     , (21347,  78,       1) /* Friction */
     , (21347,  79,       0) /* Elasticity */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (21347,   1, 'Chorizite Arrow') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (21347,   1,   33558045) /* Setup */
     , (21347,   3,  536870932) /* SoundTable */
     , (21347,   6,   67111919) /* PaletteBase */
     , (21347,   7,  268436303) /* ClothingBase */
     , (21347,   8,  100673584) /* Icon */
     , (21347,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-04-21T02:39:01.6528345-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [
    {
      "created": "2019-12-18T14:41:17.2936064-08:00",
      "author": "Zarto ",
      "comment": "-----IntStats Changes----- \nMODIFIED: (5)EncumbranceVal - from: 10 to 5 \nMODIFIED: (11)MaxStackSize - from: 250 to 1000 \nMODIFIED: (13)StackUnitEncumbrance - from: 10 to 5 \n"
    }
  ],
  "UserChangeSummary": "-----IntStats Changes----- \nMODIFIED: (5)EncumbranceVal - from: 10 to 5 \nMODIFIED: (11)MaxStackSize - from: 250 to 1000 \nMODIFIED: (13)StackUnitEncumbrance - from: 10 to 5 \n",
  "IsDone": true
}
*/
